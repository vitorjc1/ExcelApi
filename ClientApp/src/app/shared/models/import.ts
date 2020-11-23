import { Product } from './product';
export interface Import {
    id?: number,
    date: Date,
    amount: number,
    closestDeliveryDate: Date,
    total: number
    products: Product[]
}