import React, { useState } from "react";
import { server } from "../../helpers/shared";
import styles from "./Images.module.css"



const Images = ({ className, imagePaths }) => {
    const [activeImage, setActiveImage] = useState(imagePaths?.length > 0
        ? `${server}${imagePaths[0]}`
        : `${server}/images/default//default_product.png`);

    return (
        <div className={[styles.imagesContainer, className].join(' ')}>
            <img src={activeImage} className={styles.activeImage} />
            {
                imagePaths?.length > 0 &&
                <div className={styles.smallImages}>
                    {
                        imagePaths.map((imagePath) =>
                            <img
                                className={`${server}${imagePath}` !== activeImage
                                    ? styles.smallImage
                                    : [styles.smallImage, styles.smallActiveImage].join(' ')}
                                key={imagePath}
                                src={`${server}${imagePath}`}
                                onClick={() => setActiveImage(`${server}${imagePath}`) } />)
                    }
                </div>
            }
        </div>
    )
}

export default Images;