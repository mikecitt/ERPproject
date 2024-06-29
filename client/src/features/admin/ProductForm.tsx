import {
  Box,
  Paper,
  Typography,
  Grid,
  Button,
  Select,
  MenuItem,
  SelectChangeEvent,
} from "@mui/material";
import { useEffect, useState } from "react";
import { FieldValues, useForm } from "react-hook-form";
import AppTextInput from "../../app/components/AppTextInput";
import { Product } from "../../app/models/products";
import { yupResolver } from "@hookform/resolvers/yup";
import { validationSchema } from "./productValidation";
import agent from "../../app/api/agent";
import { useAppDispatch } from "../../app/store/configureStore";
import { setProduct } from "../catalog/catalogSlice";
import { LoadingButton } from "@mui/lab";

interface Props {
  product?: Product;
  cancelEdit: () => void;
}
interface Category {
  id: number;
  categoryName: string;
}
export default function ProductForm({ product, cancelEdit }: Props) {
  const {
    control,
    reset,
    handleSubmit,
    watch,
    formState: { isDirty, isSubmitting },
  } = useForm({
    resolver: yupResolver(validationSchema),
  });
  const watchFile = watch("file", null);
  const dispatch = useAppDispatch();

  const [categories, setCategories] = useState<Category[]>([]);
  const [selectedCategory, setSelectedCategory] = useState<number>(0);

  useEffect(() => {
    // Fetch categories from the backend API
    agent.Category.get()
      .then((data) => setCategories(data))
      .catch((error) => console.error("Error fetching categories:", error));
  }, []);

  const handleCategoryChange = (event: SelectChangeEvent<number>) => {
    setSelectedCategory(event.target.value as number);
  };

  useEffect(() => {
    if (product && !watchFile && !isDirty) {
      reset({ ...product, category: product.categoryId }); // Set initial form values, including the category
      setSelectedCategory(product.categoryId); // Set selected category
    }
    return () => {
      if (watchFile) URL.revokeObjectURL(watchFile.preview);
    };
  }, [product, reset, watchFile, isDirty]);

  async function handleSubmitData(data: FieldValues) {
    try {
      const formData = {
        ...data,
        categoryId: selectedCategory,
      };
      let response: Product;
      if (product) {
        response = await agent.Admin.updateProduct(formData);
      } else {
        console.log(formData);
        response = await agent.Admin.createProduct(formData);
      }
      dispatch(setProduct(response));
      cancelEdit();
    } catch (error) {
      console.log(error);
    }
  }

  return (
    <Box component={Paper} sx={{ p: 4 }}>
      <Typography variant="h4" gutterBottom sx={{ mb: 4 }}>
        Product Details
      </Typography>
      <form onSubmit={handleSubmit(handleSubmitData)}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={12}>
            <AppTextInput
              control={control}
              name="productName"
              label="Naziv proizvoda"
            />
          </Grid>
          <Grid item xs={12} sm={6}>
            <AppTextInput
              type="number"
              control={control}
              name="price"
              label="Cena"
            />
          </Grid>
          <Grid item xs={12} sm={6}>
            <AppTextInput
              type="number"
              control={control}
              name="quantityInStock"
              label="Kolicina"
            />
          </Grid>
          <Grid item xs={12} sm={12}>
            <AppTextInput
              control={control}
              name="imagePath"
              label="Link do slike proizvoda"
            />
          </Grid>
          <Grid item xs={12}>
            <AppTextInput
              control={control}
              multiline={true}
              rows={4}
              name="description"
              label="Opis"
            />
          </Grid>
          <Grid item xs={12}>
            <Typography sx={{ marginBottom: "0.5rem" }}>Kategorija:</Typography>
            <Select
              name="category"
              value={selectedCategory}
              onChange={handleCategoryChange}
              sx={{ width: "100%" }}
            >
              <MenuItem value="">
                <em>Izaberite kategoriju</em>
              </MenuItem>
              {categories.map((category) => (
                <MenuItem key={category.id} value={category.id}>
                  {category.categoryName}
                </MenuItem>
              ))}
            </Select>
          </Grid>
        </Grid>
        <Box display="flex" justifyContent="space-between" sx={{ mt: 3 }}>
          <Button onClick={cancelEdit} variant="contained" color="inherit">
            Odustani
          </Button>
          <LoadingButton
            loading={isSubmitting}
            type="submit"
            variant="contained"
            color="success"
          >
            Kreiraj
          </LoadingButton>
        </Box>
      </form>
    </Box>
  );
}
