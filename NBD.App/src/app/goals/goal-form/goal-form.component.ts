import { Component, Input } from '@angular/core';

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

    constructor(private goalsService: GoalsService) { 
        this.goal = new Goal();
    }

    onSubmit() {
        if (this.parent != null) {
            this.goal.parentId = this.parent.id;
        }

        this.goalsService.addGoal(this.goal);
    }
}