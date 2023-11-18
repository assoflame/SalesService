import React, { useEffect, useState } from "react";
import { getUserRatings } from "../../helpers/products";
import { usePagination } from "../../hooks/usePagination";
import { useFetching } from "../../hooks/useFetching";
import { getPagesCount } from "../../helpers/shared";
import styles from "./Reviews.module.css"



const Reviews = ({ reviews }) => {

    useEffect(() => {
        console.log(reviews);
    }, [reviews]);

    return (
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
    )
}


export default Reviews;