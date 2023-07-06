import { TableContainer, Paper, Table, TableBody, TableRow, TableCell } from "@mui/material";
import { useAppSelector } from "../../app/store/configureStore";

interface Props {
    subtotal?: number;
}

export default function BasketSummary({ subtotal }: Props) {
    const { basket } = useAppSelector(state => state.basket);

    if (subtotal === undefined)
        subtotal = basket?.items.reduce((sum, item) => sum + (item.quantity * item.price), 0) ?? 0;
    const deliveryFee = subtotal > 5000 ? 0 : 250;

    return (
        <>
            <TableContainer component={Paper} variant={'outlined'}>
                <Table>
                    <TableBody>
                        <TableRow>
                            <TableCell colSpan={2}>Cena</TableCell>
                            <TableCell align="right">{subtotal} RSD</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell colSpan={2}>Dostava*</TableCell>
                            <TableCell align="right">{deliveryFee} RSD</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell colSpan={2}>Ukupno</TableCell>
                            <TableCell align="right">{subtotal + deliveryFee} RSD</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>
                                <span style={{ fontStyle: 'italic' }}>*Za narudzbine preko 5000 dinara besplatna dostava</span>
                            </TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    )
}