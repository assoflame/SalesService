import React, { useEffect } from 'react';
import styles from './Header.module.css'


const Header = ({children}) => {
    return (
        <div className={styles.header}>
            <div className={styles.logo}>
                <div className={styles.leftLogo}>Sales</div>
                <div className={styles.rightLogo}>Service</div>
            </div>
            {children}
        </div>  
    )
}

export default Header;
