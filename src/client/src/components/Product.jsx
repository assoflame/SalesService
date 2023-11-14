import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getProductById, getUserRatings } from "../helpers/products";
import { getPagesCount } from "../helpers/shared";
import { usePagination } from "../hooks/usePagination";
import { useFetching } from "../hooks/useFetching";
import Loader from "./UI/Loader/Loader";


const Product = () => {
    const { id } = useParams();
    const [product, setProduct] = useState({});
    const [userRatings, setUserRatings] = useState([]);
    const [queryParams, setQueryParams] = useState({
        pageNumber: 1,
        pageSize: 5
    });

    const [totalPages, setTotalPages] = useState(0);
    const pages = usePagination(totalPages);

    const [fetchProduct, isProductLoading, fetchProductError] = useFetching(async () => {
        let product = await getProductById(id);
        setProduct(product);
    }, () => setProduct({}));

    const [fetchRatings, isRatingsLoading, fetchRatingsError] = useFetching(async () => {
        let response = await getUserRatings(product.id, queryParams);
        setUserRatings([...await response.json()]);
        const totalCount = JSON.parse(response.headers.get('X-Pagination')).TotalCount;
        setTotalPages(getPagesCount(totalCount, queryParams.pageSize));
    }, () => setUserRatings([]));

    useEffect(() => {
        fetchProductWithRatings();
    }, []);

    const fetchProductWithRatings = async () => {
        await fetchProduct();
        await fetchRatings();
    }

    return (
        <div>
            {
                isProductLoading
                    ? <Loader />
                    : <div>
                        <div>
                            {product?.imagePaths?.length > 0
                                && product.imagePaths.map((imagePath, index) => <div key={index}>{imagePath}</div>/*<img  alt='product' src={imagePath}/>*/)}
                        </div>
                        <div>
                            <div>{product?.name}</div>
                            <div>Цена: {product?.price}</div>
                            <div>Описание: {product?.description}</div>
                        </div>
                    </div>
            }
            <div>
                <div>Отзывы продавца:</div>
                {
                    isRatingsLoading
                        ? <Loader />
                        : <div>
                            <div>
                                {
                                    userRatings.map(rating => <div key={rating.id}>Кол-во звёзд: {rating.starsCount}<br />Отзыв: {rating.comment}</div>)
                                }
                            </div>
                            <div>
                                {pages?.length > 0 && pages.map(page => <span key={page}
                                    onClick={() => setQueryParams({ ...queryParams, pageNumber: page })}>{page}</span>)}
                            </div>
                        </div>
                }
            </div>
        </div>
    )
}

export default Product;
