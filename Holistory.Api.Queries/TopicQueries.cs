using Dapper;
using Holistory.Api.DataTranserObjects;
using Holistory.Api.Queries.Interfaces;
using Holistory.Common.Exceptions;
using Holistory.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holistory.Api.Queries
{
    public class TopicQueries : ITopicQueries
    {
        public const string GET_TOPICS = @"
            SELECT
            	Id,
            	Title,
            	[Description],
            	StartDate,
            	EndDate,
            	Map,
            	RegionId,
            	EraId
            FROM Topic
            WHERE UtcDateDeleted IS NULL
            AND (@topicId IS NULL OR Id = @topicId)";

        public const string GET_ATTEMPTS_FOR_USER = @"
            SELECT
                Id,
            	TopicId,
            	DateTaken,
            	Correct,
            	Incorrect
            FROM Attempt
            WHERE 
            	UserId = @userId
            	AND UtcDateDeleted IS NULL
                AND (@topicId IS NULL OR TopicId = @topicId)";

        public const string GET_QUESTIONS_FOR_TOPIC = @"
            SELECT
            	Id,
            	TopicId,
            	EventId,
            	[Text]
            FROM Question
            WHERE
            	TopicId = @topicId
            	AND UtcDateDeleted IS NULL";

        public const string GET_ANSWERS_FOR_TOPIC = @"
            SELECT
            	a.Id,
            	a.QuestionId,
            	a.[Text],
            	a.IsCorrect
            FROM Answer a
            INNER JOIN Question q
            	ON a.QuestionId = q.Id
            WHERE
            	q.TopicId = @topicId
            	AND q.UtcDateDeleted IS NULL
            	AND a.UtcDateDeleted IS NULL";

        public const string GET_EVENTS_FOR_TOPIC = @"
            SELECT
            	Id,
            	TopicId,
            	Title,
            	Content,
            	X,
            	Y,
            	EventTypeId	
            FROM [Event]
            WHERE
            	TopicId = @topicId
            	AND UtcDateDeleted IS NULL";

        private readonly IConnectionProvider _connectionProvider;

        public TopicQueries(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<TopicDto>> GetAsync(string userId)
        {
            using (IDbConnection connection = _connectionProvider.GetConnection())
            {
                int? topicId = null;
                IEnumerable<TopicDto> topics = await connection.QueryAsync<TopicDto>(GET_TOPICS, new { topicId });
                IEnumerable<AttemptDto> attempts = await connection.QueryAsync<AttemptDto>(GET_ATTEMPTS_FOR_USER, new { userId, topicId });

                ILookup<int, AttemptDto> attemptLookup = attempts.ToLookup(x => x.TopicId);

                foreach (TopicDto topic in topics)
                {
                    topic.Attempts = attemptLookup[topic.Id];
                }

                return topics;
            }
        }

        public async Task<TopicDto> GetByIdAsync(string userId, int topicId)
        {
            using (IDbConnection connection = _connectionProvider.GetConnection())
            {
                TopicDto topic = await connection.QueryFirstOrDefaultAsync<TopicDto>(GET_TOPICS, new { topicId });
                NotFoundException.ThrowIfNull(topic, nameof(topic));

                IEnumerable<EventDto> events = await connection.QueryAsync<EventDto>(GET_EVENTS_FOR_TOPIC, new { topicId });
                IEnumerable<QuestionDto> questions = await connection.QueryAsync<QuestionDto>(GET_QUESTIONS_FOR_TOPIC, new { topicId });
                IEnumerable<AnswerDto> answers = await connection.QueryAsync<AnswerDto>(GET_ANSWERS_FOR_TOPIC, new { topicId });
                IEnumerable<AttemptDto> attempts = await connection.QueryAsync<AttemptDto>(GET_ATTEMPTS_FOR_USER, new { userId, topicId });

                ILookup<int, AnswerDto> answerLokup = answers.ToLookup(x => x.QuestionId);

                foreach (QuestionDto question in questions)
                {
                    question.Answers = answerLokup[question.Id];
                }

                topic.Events = events;
                topic.Questions = questions;
                topic.Attempts = attempts;

                return topic;
            }
        }
    }
}
