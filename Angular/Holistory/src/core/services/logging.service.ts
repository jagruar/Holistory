import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class LoggingService {

    constructor() {
    }

    log(message: any) {
        if (environment.logging) {
            console.log(message);
        }
    }
}