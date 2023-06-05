import { Grid } from "@mui/material";
import { useAppSelector } from "../../app/store/configureStore";
import ProductCard from "./ProductCard";
import { Product } from "../../app/models/products";
import ProductCardSkeleton from "./ProductCardSceleton";

interface Props {
    products: Product[];
}

export default function ProductList({ products }: Props) {
    const { productsLoaded } = useAppSelector(state => state.catalog);
    return (
        <Grid container spacing={4}>
            {products.map(product => (
                <Grid key={product.id} item xs={4}>
                    {!productsLoaded ? (
                        <ProductCardSkeleton />
                    ) : (
                        <ProductCard product={product} />
                    )}
                </Grid>
            ))}
        </Grid>
    )
}