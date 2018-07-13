import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { Goal } from './goal';

@Injectable()
export class GoalsService {
    private readonly baseEndpoint = 'http://localhost:50401/api/goals';

    constructor(private http: Http) { }

    addGoal(goal: Goal): Promise<Goal> {
        return this.http
            .post(this.baseEndpoint, goal)
            .toPromise()
            .then(r => r.json() as Goal)
            .catch(err => {
                console.log(err);

                return new Goal();
            })
    }
}