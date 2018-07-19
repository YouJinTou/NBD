import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

import { GoalsService } from '../goals.service';
import { Goal } from '../goal';

@Component({
    selector: 'goal-form',
    templateUrl: './goal-form.component.html',
    styleUrls: ['./goal-form.component.css']
})
export class GoalFormComponent {
    @Input() parent: Goal;
    goal: Goal;

    constructor(private goalsService: GoalsService, private router: Router) {
        this.goal = new Goal();
    }

    onSubmit() {
        if (this.parent != null) {
            this.goal.parentId = this.parent.id;
        }

        this.goalsService.addGoal(this.goal).subscribe((r: Goal) => {
            this.router.navigate(['trees', r.id])
        });
    }
}