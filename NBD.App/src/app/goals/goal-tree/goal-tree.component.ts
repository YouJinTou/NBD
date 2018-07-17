import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Goal } from '../goal';
import { GoalsService } from '../goals.service';

@Component({
    selector: 'goal-tree',
    styleUrls: ['./goal-tree.component.css'],
    templateUrl: './goal-tree.component.html'
})
export class GoalTreeComponent implements OnInit {
    private root: Goal;

    constructor (private route: ActivatedRoute, private goalsService: GoalsService) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.goalsService.getGoal(params['id']).then(r => this.root = r);
        });
    }
}