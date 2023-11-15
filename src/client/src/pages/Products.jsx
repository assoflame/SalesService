import React, { useEffect, useState } from "react";
import { getProducts } from "../helpers/products";
import ProductCard from "../components/ProductCard";
import { useFetching } from "../hooks/useFetching";
import { Select } from "../components/UI/Select/Select";
import { getPagesCount } from "../helpers/shared";
import { usePagination } from "../hooks/usePagination";
import Loader from "../components/UI/Loader/Loader";
import Header from '../components/UI/Header/Header';
import Menu from "../components/UI/Menu/Menu";


export const Products = () => {
    const [products, setProducts] = useState([]);
    const [queryParams, setQueryParams] = useState({
        searchString: '',
        minPrice: '',
        maxPrice: '',
        pageNumber: 1,
        pageSize: 10,
        orderBy: ''
    });

    const [totalPages, setTotalPages] = useState(0);
    const pages = usePagination(totalPages);

    const [fetchProducts, isProductsLoading, productsError] = useFetching(async () => {
        const response = await getProducts(queryParams);
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
            <Header/>
            <Menu/>
            <div>
                <div>
                    <input onChange={e => setQueryParams({ ...queryParams, searchString: e.target.value })} placeholder="Поиск..." />
                    <input onChange={e => setQueryParams({ ...queryParams, minPrice: e.target.value })} placeholder="Минимальная цена" />
                    <input onChange={e => setQueryParams({ ...queryParams, maxPrice: e.target.value })} placeholder="Максимальная цена" />
                </div>
                <Select
                    value={queryParams.orderBy}
                    onChange={changeSort}
                    defaultValue="Сортировка"
                    options={[
                        { value: 'price', name: 'По цене' },
                        { value: 'name', name: 'По названию' },
                        { value: 'date', name: 'По дате' }
                    ]} />
                <button onClick={async () => await fetchProducts()}>Найти</button>
            </div>
            {productsError && <div>Ошибка загрузки товаров</div>}
            {
                isProductsLoading
                    ? <Loader/>
                    : <div>
                        <div>
                            {pages?.length > 0 && pages.map(page => <span key={page}
                                onClick={() => setQueryParams({ ...queryParams, pageNumber: page })}>{page}</span>)}
                        </div>
                        <div>
                            {products.map(product => <div key={product.id}><ProductCard product={product} /><br /></div>)}
                        </div>
                    </div>
            }
        </>
    )
}

export default Products;