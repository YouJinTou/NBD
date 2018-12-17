export class Goal {
    id: string;
    parentId: string;
    title: string;
    startDate: string;
    endDate: string;
    description: string;
    recurrenceType: number;
    recurrenceValue: number;
    target: number;
    progress: number;
    subGoals: Goal[];
    isReached: boolean;
}