import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'recurrence' })
export class RecurrencePipe implements PipeTransform {
    transform(value: number, type: number): string {
        var isPlural = value != 0;

        switch (type) {
            case 0: return "Once";
            case 1: return "Every " + (isPlural ? value + " days" : "day");
            case 2: return "Every " + (isPlural ? value + " weeks" : "week");
            case 3: return "Every " + (isPlural ? value + " months" : "month");
            case 4: return "Every " + (isPlural ? value + " years" : "year");
        }
    }
}