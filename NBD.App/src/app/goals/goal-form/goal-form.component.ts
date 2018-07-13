import { Component } from '@angular/core';

import { GoalsService } from '../goals.service';
import { Goal } from '../goal';

@Component({
    selector: 'goal-form',
    templateUrl: './goal-form.component.html',
    styleUrls: ['./goal-form.component.css']
})
export class GoalFormComponent {
    goal: Goal;

    constructor(private goalsService: GoalsService) { 
        this.goal = new Goal();
    }

    onSubmit() {
        this.goalsService.addGoal(this.goal);
    }
}