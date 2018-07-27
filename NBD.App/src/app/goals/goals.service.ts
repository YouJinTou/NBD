import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';
import { Observable } from 'rxjs';
import 'rxjs/add/observable/of';

import { Goal } from './goal';

@Injectable()
export class GoalsService {
    private readonly baseEndpoint = 'http://localhost:50401/api';

    constructor(private http: HttpClient) { }

    getGoal(id: string): Observable<Goal> {
        var endpoint = this.baseEndpoint + '/goals/' + id;

        return this.http.get<Goal>(endpoint);
    }

    addGoal(goal: Goal): Observable<Goal> {
        var endpoint = this.baseEndpoint + '/goals';

        return this.http.post<Goal>(endpoint, goal);
    }

    makeProgress(id: string, progress: number): Observable<Goal> {
        var endpoint = this.baseEndpoint + '/goals/progress';
        var body = {
            goalId: id,
            progress: progress == null ? 1 : progress
        };
        
        return this.http.post<Goal>(endpoint, body);
    }
}