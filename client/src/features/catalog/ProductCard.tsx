import { Avatar, Button, Card, CardActions, CardContent, CardMedia, Typography, CardHeader } from "@mui/material";
import { Product } from "../../app/models/products";
import { Link } from "react-router-dom";
import { LoadingButton } from "@mui/lab";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { addBasketItemAsync } from "../basket/basketSlice";

interface Props {
    product: Product
}
export default function ProductCard({ product }: Props) {

    const { status } = useAppSelector(state => state.basket)
    const dispatch = useAppDispatch();

    return (
        <Card>
            <CardHeader
                avatar=
                {
                    <Avatar sx={{ bgcolor: 'secondary.main' }}>
                        {product.productName.charAt(0).toUpperCase()}
                    </Avatar>
                }
                title={product.productName}
                titleTypographyProps={{
                    sx: { fontWeight: 'bold', color: 'primary.main' }
                }}
            />
            <CardMedia
                sx={{ height: 140, backgroundSize: 'contain', bgcolor: 'primary.light' }}
                image={product.imagePath}
                title={product.productName}
            />
            <CardContent>
                <Typography gutterBottom variant="h5" color='secondary'>
                    {product.price} RSD
                </Typography>
            </CardContent>
            <CardActions>
                <LoadingButton loading={status.includes('pendingAddItem'+product.id)}
                    onClick={() => dispatch(addBasketItemAsync({productId: product.id,}))}
                    size="small">
                    Add to cart 
                </LoadingButton>
                <Button component={Link} to={`/catalog/${product.id}`} size="small">View</Button>
            </CardActions>
        </Card>
    )
}