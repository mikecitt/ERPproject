import { Edit, Delete } from "@mui/icons-material";
import { LoadingButton } from "@mui/lab";
import { Box, Typography, Button, TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody } from "@mui/material";
import { useState } from "react";
import agent from "../../app/api/agent";
import AppPagination from "../../app/components/AppPagination";
import useProducts from "../../app/hooks/useProducts";
import { Product } from "../../app/models/products";
import { useAppDispatch } from "../../app/store/configureStore";
import { removeProduct, setPageNumber } from "../catalog/catalogSlice";
import ProductForm from "./ProductForm";

export default function Inventory() {
    const {products, metaData} = useProducts();
    const dispatch = useAppDispatch();
    const [editMode, setEditMode] = useState(false);
    const [selectedProduct, setSelectedProduct] = useState<Product | undefined>(undefined);
    const [loading, setLoading] = useState(false);
    const [target, setTarget] = useState(0);

    function handleSelectProduct(product: Product) {
        setSelectedProduct(product);
        setEditMode(true);
    }

    function handleDeleteProduct(id: number) {
        setLoading(true);
        setTarget(id)
        agent.Admin.deleteProduct(id)
            .then(() => dispatch(removeProduct(id)))
            .catch(error => console.log(error))
            .finally(() => setLoading(false))
    }

    function cancelEdit() {
        if (selectedProduct) setSelectedProduct(undefined);
        setEditMode(false);
    }

    if (editMode) return <ProductForm product={selectedProduct} cancelEdit={cancelEdit} />

    return (
        <>
            <Box display='flex' justifyContent='space-between'>
                <Typography sx={{ p: 2 }} variant='h4'>Proizvodi</Typography>
                <Button onClick={() => setEditMode(true)} sx={{ m: 2 }} size='large' variant='contained'>Kreiraj proizvod</Button>
            </Box>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>#</TableCell>
                            <TableCell align="left">Proizvod</TableCell>
                            <TableCell align="right">Cena</TableCell>
                            <TableCell align="right"></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {products.map((product) => (
                            <TableRow
                                key={product.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {product.id}
                                </TableCell>
                                <TableCell align="left">
                                    <Box display='flex' alignItems='center'>
                                        <img src={product.imagePath} alt={product.productName} style={{ height: 50, marginRight: 20 }} />
                                        <span>{product.productName}</span>
                                    </Box>
                                </TableCell>
                                <TableCell align="right">{product.price}</TableCell>
                                <TableCell align="right">
                                    <Button onClick={() => handleSelectProduct(product)} startIcon={<Edit />} />
                                    <LoadingButton 
                                        loading={loading && target === product.id} 
                                        onClick={() => handleDeleteProduct(product.id)} 
                                        startIcon={<Delete />} color='error' />
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            {metaData && 
                <Box sx={{pt: 2}}>
                    <AppPagination 
                        metaData={metaData} 
                        onPageChange={(page: number) => dispatch(setPageNumber({pageNumber: page}))} />
                </Box>
            }
        </>
    )
}