import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getProductById } from "../../api/products";
import { useFetching } from "../../hooks/useFetching";
import Loader from "../UI/Loader/Loader";
import Images from "./Images";
import Header from "../UI/Header/Header";
import Logout from "../Auth/Logout";
import styles from "./Product.module.css"
import Menu from "../UI/Menu/Menu"
import Button from "../UI/Button/Button";
import Modal from "../UI/Modal/Modal"
import ModalMessage from "../ModalMessage/ModalMessage"
import Reviews from "../Reviews/Reviews";
import { isAdmin, trySendAuthorizedRequest } from "../../api/auth";
import { blockUser } from "../../api/admin";


const Product = () => {
    const { id } = useParams();
    const [product, setProduct] = useState({});

    const [messageVisible, setMessageVisible] = useState(false);

    const [fetchProduct, isProductLoading, fetchProductError] = useFetching(async () => {
        let fetchedProduct = await trySendAuthorizedRequest(getProductById, id)
        setProduct(fetchedProduct);
    }, () => setProduct({}));

    useEffect(() => {
        fetchProduct();
    }, []);

    return (
        <>
            <Header><Logout /></Header>
            <Menu />
            {
                isProductLoading
                    ? <div className={styles.loaderContainer}><Loader /></div>
                    : Object.entries(product).length > 0 &&
                    <div>
                        <div className={styles.productContainer}>
                            <Images className={styles.images} imagePaths={product?.imagePaths} />
                            <div className={styles.productInfoContainer}>
                                <div className={styles.firstInfo}>
                                    <div id={styles.name}>{product?.name}</div>
                                    <div id={styles.description}>Описание: {product?.description}</div>
                                </div>
                                <div className={styles.secondInfo}>
                                    <div id={styles.price}>{`${product?.price} руб.`}</div>
                                    <div>{`Опубликовано: ${new Date(product?.creationDate).toLocaleString()}`}</div>
                                    {product.userId != localStorage['id'] &&
                                        <Button callback={() => setMessageVisible(true)}>Написать продавцу</Button>}
                                </div>
                                {isAdmin() && product.userId != localStorage['id'] &&
                                    <Button classNames={styles.blockUserButton}
                                        callback={async () => await blockUser(product.userId)}>Заблокировать пользователя
                                    </Button>}
                                <Modal visible={messageVisible} setVisible={setMessageVisible}>
                                    <ModalMessage sellerId={product?.userId} />
                                </Modal>
                            </div>
                        </div>
                        <Reviews className={styles.reviews} userId={product.userId} key={product?.userId} />
                    </div>
            }
            {fetchProductError && <div className={styles.error}>Ошибка загрузки продукта</div>}
        </>
    )
}

export default Product;
