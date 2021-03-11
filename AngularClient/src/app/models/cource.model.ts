export class Course {
    courseId: number = 0;
    udemyLink: string = "";
    title: string = "";
    headline: string = "";
    description: string = "";
    avg_rating_recent: number = 0;
    image_200_H: string = "";
    category: string = "";
    endTime: Date = new Date();
    originalPrice: number = 0;
    discountedPrice: number = 0;
    amount: number = 0;
    createdDate: Date = new Date();
    remainingTime: Date = new Date();
    discount_percent: number = 0;
}