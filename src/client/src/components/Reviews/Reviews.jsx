import React, { useEffect, useState } from "react";
import { getUserReviews } from "../../helpers/products";
import { usePagination } from "../../hooks/usePagination";
import { useFetching } from "../../hooks/useFetching";
import { getPagesCount } from "../../helpers/shared";
import styles from "./Reviews.module.css"
import PageNumbersList from "../UI/Paging/PageNumbersList/PageNumbersList";
import Button from "../UI/Button/Button";
import Modal from "../UI/Modal/Modal";
import ModalReview from "../ModalReview/ModalReview";



const Reviews = ({ className, userId }) => {
    const [reviews, setReviews] = useState([]);
    const [queryParams, setQueryParams] = useState({
        pageNumber: 1,
        pageSize: 5
    });

    const [totalPages, setTotalPages] = useState(0);
    const pages = usePagination(totalPages);
    const [reviewVisible, setReviewVisible] = useState(false);

    const [fetchReviews, isReviewsLoading, fetchReviewsError] = useFetching(async () => {
        let response = await getUserReviews(userId, queryParams);
        setReviews([...await response.json()]);
        let totalCount = JSON.parse(response.headers.get("X-Pagination")).TotalCount;
        console.log(JSON.parse(response.headers.get("X-Pagination")));
        setTotalPages(getPagesCount(totalCount, queryParams.pageSize));
    }, () => setReviews([]));

    useEffect(() => {
        if (userId)
            fetchReviews();
    }, [queryParams.pageNumber]);

    return (
        <div className={[styles.reviewsContainer, className].join(' ')}>
            <div className={styles.reviewsHeader}>
                <h1 className={styles.reviewsTitle}>Отзывы продавца</h1>
                <Button callback={() => setReviewVisible(true)}>Оставить отзыв</Button>
            </div>
            <div className={styles.list}>
                {
                    reviews.length > 0
                    && reviews.map(review =>
                        <div key={review.user.id} className={styles.review}>
                            <div>{review.user.fullName}, {review.user.city}</div>
                            <div>Оценка: {review.starsCount}</div>
                            <div className={styles.commentWithDate}>
                                <div>Комментарий: {review.comment}</div>
                                <div>{new Date(review.creationDate).toLocaleString()}</div>
                            </div>
                        </div>
                    )
                }
            </div>
            <PageNumbersList
                classNames={styles.pagesList}
                pages={pages}
                activePage={queryParams.pageNumber}
                setPage={(page) => setQueryParams({ ...queryParams, pageNumber: page })} />
            <Modal visible={reviewVisible} setVisible={setReviewVisible}><ModalReview sellerId={userId} /></Modal>
        </div>
    )
}


export default Reviews;