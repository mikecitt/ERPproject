import * as yup from 'yup';

export const validationScheme = [
    yup.object({
        fullName: yup.string().required('Ime je obavezno'),
        address1: yup.string().required('Adresa je obavezna'),
        address2: yup.string().required(),
        city: yup.string().required(),
        state: yup.string().required(),
        zip: yup.number().required().typeError('Postanski broj mora biti broj'),
        country: yup.string().required()
    })
]