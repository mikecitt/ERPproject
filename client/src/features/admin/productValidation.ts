import * as yup from 'yup';

export const validationSchema = yup.object({
    productName: yup.string().required(),
    price: yup.number().required().moreThan(100),
    quantityInStock: yup.number().required().min(0),
    description: yup.string().required()
})