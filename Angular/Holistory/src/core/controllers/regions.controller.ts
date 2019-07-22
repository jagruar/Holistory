import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LoggingService } from '../services/logging.service';
import { Region } from '../models/dtos/region';

@Injectable({ providedIn: 'root' })
export class RegionsController {
    private regions = new BehaviorSubject<Region[]>(null);
    private regions$ = this.regions.asObservable();

    constructor(
        private http: HttpClient,
        private logger: LoggingService) {
    }

    public getRegions(): Observable<Region[]> {
        if (this.regions.value) {
            this.logger.log("regions exists");
            return this.regions$;
        } else {
            this.logger.log("making call for regions");
            return this.http.get<Region[]>(`${environment.apiUrl}/regions`)
                .pipe(map(x => {
                    this.regions.next(x);
                    return x;   
                }));            
        }
    }
}