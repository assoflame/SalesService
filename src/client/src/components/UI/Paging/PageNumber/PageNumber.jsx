import React from "react";
import styles from "./PageNumber.module.css"


const PageNumber = ({isActive, page, setPage}) => {
    return (
        <span className={isActive ? [styles.activePage, styles.page].join(' ') : styles.page} onClick={() => setPage(page)}>{page}</span>
    )
}

export default PageNumber;