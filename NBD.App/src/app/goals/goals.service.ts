import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { Goal } from './goal';
import { GoalTree } from './goal-tree';

@Injectable()
export class GoalsService {
    private readonly baseEndpoint = 'http://localhost:50401/api/';

    constructor(private http: Http) { }

    getGoal(id: string): Promise<Goal> {
        var endpoint = this.baseEndpoint + '/goals/' + id;

        return this.http
            .get(endpoint)
            .toPromise()
            .then(r => r.json() as Goal)
            .catch(err => {
                console.log(err);

                return new Goal();
            });
    }

    addGoal(goal: Goal): Promise<Goal> {
        var endpoint = this.baseEndpoint + '/goals';

        return this.http
            .post(endpoint, goal)
            .toPromise()
            .then(r => r.json() as Goal)
            .catch(err => {
                console.log(err);

                return new Goal();
            });
    }

    createTree(tree: GoalTree): Promise<GoalTree> {
        var endpoint = this.baseEndpoint + '/goalTrees';

        return this.http
            .post(endpoint, tree)
            .toPromise()
            .then(r => r.json() as GoalTree)
            .catch(err => {
                console.log(err);

                return new GoalTree();
            });
    }
}