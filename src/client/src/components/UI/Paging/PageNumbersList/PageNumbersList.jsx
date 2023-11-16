import React from "react";
import PageNumber from "../PageNumber/PageNumber";
import styles from "./PageNumberList.module.css"



const PageNumbersList = ({classNames, activePage, pages, setPage}) => {
    return (
        <div className={[classNames, styles.list].join(' ')}>
            {
                pages.map(page => <PageNumber isActive={activePage === page} key={page} page={page} setPage={setPage} />)
            }
        </div>
    )
}

export default PageNumbersList;
