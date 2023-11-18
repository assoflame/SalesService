import React from "react";
import styles from "./Loader.module.css"


const Loader = ({className}) => {
    return (
        <div className={[className,styles.loader].join(' ')}>
        </div>
    )
}

export default Loader;
