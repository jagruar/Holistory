import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LoggingService } from '../services/logging.service';
import { Era } from '../models/dtos/era';

@Injectable({ providedIn: 'root' })
export class ErasController {
    private eras = new BehaviorSubject<Era[]>(null);
    private eras$ = this.eras.asObservable();

    constructor(
        private http: HttpClient,
        private logger: LoggingService) {
    }

    public getEras(): Observable<Era[]> {
        if (this.eras.value) {
            this.logger.log("eras exists");
            return this.eras$;
        } else {
            this.logger.log("making call for eras");
            return this.http.get<Era[]>(`${environment.apiUrl}/eras`)
                .pipe(map(x => {
                    this.eras.next(x);
                    return x;   
                }));            
        }
    }
}