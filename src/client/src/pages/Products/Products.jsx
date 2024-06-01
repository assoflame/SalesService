import React, { useEffect, useState } from "react";
import { getProducts } from "../../api/products";
import { useFetching } from "../../hooks/useFetching";
import { Select } from "../../components/UI/Select/Select";
import { getPagesCount } from "../../api/shared";
import { usePagination } from "../../hooks/usePagination";
import Loader from "../../components/UI/Loader/Loader";
import Header from '../../components/UI/Header/Header';
import Menu from "../../components/UI/Menu/Menu";
import ProductsList from "../../components/Product/ProductsList";
import styles from "./Products.module.css"
import PageNumbersList from "../../components/UI/Paging/PageNumbersList/PageNumbersList";
import Modal from "../../components/UI/Modal/Modal";
import ProductCreationForm from "../../components/Product/ProductCreationForm";
import Button from "../../components/UI/Button/Button";
import Logout from "../../components/Auth/Logout"
import { trySendAuthorizedRequest } from "../../api/auth";


export const Products = () => {
    const [products, setProducts] = useState([]);
    const [queryParams, setQueryParams] = useState({
        searchString: '',
        minPrice: '',
        maxPrice: '',
        pageNumber: 1,
        pageSize: 5,
        orderBy: ''
    });

    const [totalPages, setTotalPages] = useState(0);
    const pages = usePagination(totalPages);
    const [productCreationVisible, setProductCreationVisible] = useState(false);

    const [fetchProducts, isProductsLoading, productsError] = useFetching(async () => {
        const response = await trySendAuthorizedRequest(getProducts, queryParams)
        setProducts([...await response.json()]);
        const totalCount = JSON.parse(response.headers.get('X-Pagination')).TotalCount;
        setTotalPages(getPagesCount(totalCount, queryParams.pageSize));
    }, () => setProducts([]));

    const changeSort = (sort) => {
        setQueryParams({ ...queryParams, orderBy: sort });
    }

    useEffect(() => {
        fetchProducts();
    }, [queryParams.pageNumber])

    return (
        <>
            <Header><Logout/></Header>
            <Menu />
            <div className={styles.container}>
                <div className={styles.searchParams}>
                    <Select
                        value={queryParams.orderBy}
                        onChange={changeSort}
                        defaultValue="Сортировка"
                        options={[
                            { value: 'price', name: 'По цене' },
                            { value: 'name', name: 'По названию' },
                            { value: 'date', name: 'По дате' }
                        ]} />
                    <input className={styles.input} onChange={e => setQueryParams({ ...queryParams, minPrice: e.target.value })} placeholder="Мин. цена" />
                    <input className={styles.input} onChange={e => setQueryParams({ ...queryParams, maxPrice: e.target.value })} placeholder="Макс. цена" />
                    <input className={styles.input} onChange={e => setQueryParams({ ...queryParams, searchString: e.target.value })} placeholder="Поиск..." />
                    <Button callback={async () => {setQueryParams({...queryParams, pageNumber: 1}); await fetchProducts();}}>Найти</Button>
                </div>
                <Button callback={() => setProductCreationVisible(true)}>Создать объявление</Button>
                <Modal visible={productCreationVisible} setVisible={setProductCreationVisible}>
                    <ProductCreationForm/>
                </Modal>

                {productsError && <div>Ошибка загрузки</div>}
                {
                    isProductsLoading
                        ? <Loader className={styles.loader}/>
                        : <div className={styles.productsPage}>
                            <ProductsList products={products} />
                            <PageNumbersList
                                classNames={styles.pagesList}
                                pages={pages}
                                activePage={queryParams.pageNumber}
                                setPage={(page) => setQueryParams({ ...queryParams, pageNumber: page })} />
                        </div>
                }
            </div>
        </>
    )
}

export default Products;