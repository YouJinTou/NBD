import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { GoalTreeFormComponent } from './goals/goal-tree-form/goal-tree-form.component';
import { GoalFormComponent } from './goals/goal-form/goal-form.component';
import { GoalsService } from './goals/goals.service';

@NgModule({
  declarations: [
    AppComponent,
    GoalTreeFormComponent,
    GoalFormComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [GoalsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
