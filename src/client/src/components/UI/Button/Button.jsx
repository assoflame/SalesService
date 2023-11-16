import React from "react";
import styles from "./Button.module.css"


const Button = ({ classNames, children, callback }) => {
    return (
        <button className={[classNames, styles.button].join(' ')} onClick={callback}>{children}</button>
    )
}

export default Button;