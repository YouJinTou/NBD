import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';
import { Observable } from 'rxjs';

import { Goal } from './goal';

@Injectable()
export class GoalsService {
    constructor(
        private http: HttpClient, 
        @Inject('BASE_TRACKER_API_URL') private baseEndpoint: string) {
    }

    getGoal(id: string): Observable<Goal> {
        var endpoint = this.baseEndpoint + '/goals/' + id;

        return this.http.get<Goal>(endpoint, { withCredentials: true });
    }

    addGoal(goal: Goal): Observable<Goal> {
        var endpoint = this.baseEndpoint + '/goals';

        return this.http.post<Goal>(endpoint, goal, { withCredentials: true });
    }

    editGoal(goal: Goal): Observable<Goal> {
        var endpoint = this.baseEndpoint + '/goals/' + goal.id;

        return this.http.put<Goal>(endpoint, goal, { withCredentials: true });
    }

    deleteGoal(id: string) {
        var endpoint = this.baseEndpoint + '/goals/' + id;

        return this.http.delete(endpoint, { withCredentials: true });
    }

    makeProgress(id: string, progress: number): Observable<Goal> {
        var endpoint = this.baseEndpoint + '/goals/progress';
        var body = {
            goalId: id,
            progress: progress == null ? 1 : progress
        };

        return this.http.post<Goal>(endpoint, body, { withCredentials: true });
    }
}