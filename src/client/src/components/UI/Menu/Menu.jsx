import React from "react";
import { NavLink } from "react-router-dom";
import styles from './Menu.module.css'


const Menu = () => {
    return (
        <div className={styles.menu}>
            <NavLink className={styles.menuLink} to='/products'>Товары</NavLink>
            <NavLink className={styles.menuLink} to='/messages'>Сообщения</NavLink>
        </div>
    )
}

export default Menu;