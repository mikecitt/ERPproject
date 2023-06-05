import { TableContainer, Paper, Table, TableBody, TableRow, TableCell } from "@mui/material";
import { useAppSelector } from "../../app/store/configureStore";



export default function BasketSummary() {

    const {basket} = useAppSelector(state => state.basket);
    const subtotal = basket?.items.reduce((sum,item) => sum +(item.quantity*item.price),0) ?? 0
    const deliveryFee = subtotal> 3000 ? 0 : 300;

    return (
        <>
            <TableContainer component={Paper} variant={'outlined'}>
                <Table>
                    <TableBody>
                        <TableRow>
                            <TableCell colSpan={2}>Iznos</TableCell>
                            <TableCell align="right">{subtotal} RSD</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell colSpan={2}>Dostava*</TableCell>
                            <TableCell align="right">{deliveryFee} RSD</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell colSpan={2}>Ukupan iznos</TableCell>
                            <TableCell align="right">{subtotal + deliveryFee} RSD</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>
                                <span style={{ fontStyle: 'italic' }}>*Za narudzbine preko 3000 RSD dostava je besplatna</span>
                            </TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    )
}