import React, { useState, useEffect } from 'react';
import {
    Box,
    Typography,
    Paper,
    Button,
    TextField,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    IconButton,
} from '@mui/material';
import { Product, Component as ComponentType, Subcomponent } from '../models/types';
import { getProducts, createProduct, updateProduct, deleteProduct } from '../services/api';
import { v4 as uuidv4 } from 'uuid';
import DeleteIcon from '@mui/icons-material/Delete';

const ProductGrid: React.FC = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [open, setOpen] = useState(false);
    const [editingProduct, setEditingProduct] = useState<Product | null>(null);

    // Form state
    const [productName, setProductName] = useState('');
    const [productPrice, setProductPrice] = useState('');
    const [components, setComponents] = useState<ComponentType[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            const data = await getProducts();
            setProducts(data);
        };
        fetchData();
    }, []);

    const resetForm = () => {
        setProductName('');
        setProductPrice('');
        setComponents([]);
        setEditingProduct(null);
    };

    const handleAddComponent = () => {
        setComponents([
            ...components,
            {
                id: uuidv4(),
                name: '',
                quantity: 1,
                subcomponents: [],
            },
        ]);
    };

    const handleAddSubcomponent = (componentId: string) => {
        setComponents((prev) =>
            prev.map((comp) =>
                comp.id === componentId
                    ? {
                        ...comp,
                        subcomponents: [
                            ...comp.subcomponents,
                            {
                                id: uuidv4(),
                                name: '',
                                material: '',
                                notes: '',
                                count: 1,
                                totalQuantity: 1,
                                cuttingThickness: 0,
                                detailLength: 0,
                                detailWidth: 0,
                                detailThickness: 0,
                                cuttingLength: 0,
                                cuttingWidth: 0,
                                finalThickness: 0,
                                finalLength: 0,
                                finalWidth: 0,
                                veneerInner: "string",
                                veneerOuter: "string",
                            },
                        ],
                    }
                    : comp
            )
        );
    };

    const handleDeleteComponent = (id: string) => {
        setComponents((prev) => prev.filter((c) => c.id !== id));
    };

    const handleDeleteSubcomponent = (componentId: string, subId: string) => {
        setComponents((prev) =>
            prev.map((comp) =>
                comp.id === componentId
                    ? {
                        ...comp,
                        subcomponents: comp.subcomponents.filter((s) => s.id !== subId),
                    }
                    : comp
            )
        );
    };

    const handleSubmit = async () => {
        const product: Product = {
            id: editingProduct ? editingProduct.id : uuidv4(),
            name: productName,
            price: parseFloat(productPrice),
            components,
        };

        if (editingProduct) {
            const updated = await updateProduct(product);
            setProducts((prev) => prev.map((p) => (p.id === updated.id ? updated : p)));
        } else {
            const created = await createProduct(product);
            setProducts((prev) => [...prev, created]);
        }

        setOpen(false);
        resetForm();
    };

    const handleEdit = (product: Product) => {
        setEditingProduct(product);
        setProductName(product.name);
        setProductPrice(product.price.toString());
        setComponents(product.components);
        setOpen(true);
    };

    const handleDelete = async (id: string) => {
        await deleteProduct(id);
        setProducts((prev) => prev.filter((p) => p.id !== id));
    };

    return (
        <Box sx={{ padding: 2 }}>
            <Typography variant="h4" gutterBottom>
                Furniture Product Manager
            </Typography>

            <Button variant="contained" onClick={() => setOpen(true)} sx={{ mb: 2 }}>
                + Add Product
            </Button>

            {products.map((product) => (
                <Paper key={product.id} sx={{ mb: 3, p: 2 }}>
                    <Box display="flex" justifyContent="space-between">
                        <Typography variant="h6">
                            {product.name} — ${product.price}
                        </Typography>
                        <Box>
                            <Button size="small" onClick={() => handleEdit(product)} sx={{ mr: 1 }}>
                                Edit
                            </Button>
                            <Button size="small" color="error" onClick={() => handleDelete(product.id)}>
                                Delete
                            </Button>
                        </Box>
                    </Box>

                    {product.components.map((comp) => (
                        <Box key={comp.id} sx={{ ml: 2, mt: 1 }}>
                            <Typography variant="subtitle1">
                                Component: {comp.name} (Qty: {comp.quantity})
                            </Typography>
                            {comp.subcomponents.map((sub) => (
                                <Box key={sub.id} sx={{ ml: 3 }}>
                                    <Typography variant="body2">
                                        🔹 {sub.name} | {sub.material} | Count: {sub.count}
                                    </Typography>
                                </Box>
                            ))}
                        </Box>
                    ))}
                </Paper>
            ))}

            <Dialog open={open} onClose={() => { setOpen(false); resetForm(); }} maxWidth="md" fullWidth>
                <DialogTitle>{editingProduct ? 'Edit Product' : 'Create Product'}</DialogTitle>
                <DialogContent>
                    <TextField
                        label="Product Name"
                        fullWidth
                        value={productName}
                        onChange={(e) => setProductName(e.target.value)}
                        sx={{ mb: 2 }}
                    />
                    <TextField
                        label="Price"
                        type="number"
                        fullWidth
                        value={productPrice}
                        onChange={(e) => setProductPrice(e.target.value)}
                        sx={{ mb: 2 }}
                    />

                    <Button onClick={handleAddComponent} sx={{ mb: 2 }}>
                        + Add Component
                    </Button>

                    {components.map((comp, compIndex) => (
                        <Paper key={comp.id} sx={{ p: 2, mb: 2 }}>
                            <Box display="flex" alignItems="center">
                                <TextField
                                    label="Component Name"
                                    value={comp.name}
                                    onChange={(e) => {
                                        const updated = [...components];
                                        updated[compIndex].name = e.target.value;
                                        setComponents(updated);
                                    }}
                                    sx={{ mr: 2 }}
                                />
                                <TextField
                                    label="Quantity"
                                    type="number"
                                    value={comp.quantity}
                                    onChange={(e) => {
                                        const updated = [...components];
                                        updated[compIndex].quantity = parseInt(e.target.value);
                                        setComponents(updated);
                                    }}
                                    sx={{ width: 100, mr: 2 }}
                                />
                                <IconButton onClick={() => handleDeleteComponent(comp.id)} color="error">
                                    <DeleteIcon />
                                </IconButton>
                            </Box>

                            <Button onClick={() => handleAddSubcomponent(comp.id)} sx={{ mt: 1 }}>
                                + Add Subcomponent
                            </Button>

                            {comp.subcomponents.map((sub, subIndex) => (
                                <Box key={sub.id} sx={{ ml: 2, mt: 1 }}>
                                    <TextField
                                        label="Subcomponent Name"
                                        value={sub.name}
                                        onChange={(e) => {
                                            const updated = [...components];
                                            updated[compIndex].subcomponents[subIndex].name = e.target.value;
                                            setComponents(updated);
                                        }}
                                        sx={{ mr: 2 }}
                                    />
                                    <TextField
                                        label="Material"
                                        value={sub.material}
                                        onChange={(e) => {
                                            const updated = [...components];
                                            updated[compIndex].subcomponents[subIndex].material = e.target.value;
                                            setComponents(updated);
                                        }}
                                        sx={{ mr: 2 }}
                                    />
                                    <IconButton
                                        onClick={() => handleDeleteSubcomponent(comp.id, sub.id??"")}
                                        color="error"
                                    >
                                        <DeleteIcon />
                                    </IconButton>
                                </Box>
                            ))}
                        </Paper>
                    ))}
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => { setOpen(false); resetForm(); }}>Cancel</Button>
                    <Button onClick={handleSubmit}>{editingProduct ? 'Update' : 'Create'}</Button>
                </DialogActions>
            </Dialog>
        </Box>
    );
};

export default ProductGrid;
