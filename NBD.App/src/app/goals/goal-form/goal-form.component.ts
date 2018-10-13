import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

import { GoalsService } from '../goals.service';
import { Goal } from '../goal';

@Component({
    selector: 'goal-form',
    templateUrl: './goal-form.component.html',
    styleUrls: ['./goal-form.component.css']
})
export class GoalFormComponent implements OnInit {
    @Input() parent: Goal;
    @Input() isEdit: boolean;
    goal: Goal;
    private submitText: string;

    constructor(private goalsService: GoalsService, private router: Router, private location: Location) {
        this.goal = new Goal();
        this.submitText = 'Add goal';
    }

    ngOnInit() {
        if (!this.parent) {
            return;
        }

        this.goal.parentId = this.parent.id;

        if (this.isEdit) {
            this.submitText = 'Edit goal';
            this.goal = this.parent;
        }
    }

    onSubmit() {
        if (this.isEdit) {
            this.goalsService.editGoal(this.goal).subscribe((r: Goal) => {
                location.reload();
            }, error => {
                location.reload();
            });
        } else {
            this.goalsService.addGoal(this.goal).subscribe((r: Goal) => {
                location.reload();
            }, error => {
                location.reload();
            });
        }
    }
}