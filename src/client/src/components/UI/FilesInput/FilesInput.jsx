import React, { useEffect, useState } from "react";
import styles from "./FilesInput.module.css"
import Button from "../Button/Button";



const FilesInput = ({ uploadedFiles, setUploadedFiles }) => {
    const maxFilesCount = 5;
    const maxFileSize = 2 * 1024 * 1024; // 2 Mb

    const uploadFiles = (event) => {
        let choosenFiles = [...event.target.files];

        if (uploadedFiles.length + choosenFiles.length > maxFilesCount) {
            alert(`Вы не можете добавить к продукту более ${maxFilesCount} изображений.`);
            return;
        }

        if (choosenFiles.some(file => file.size > maxFileSize)) {
            alert(`Максимальный размер одного файла - ${(maxFileSize / 1024 / 1024).toFixed(2)} Мб.`);
            return;
        }

        if (uploadedFiles.some(uploadedFile =>
            choosenFiles.some(choosenFile => choosenFile.name === uploadedFile.name))) {
            alert('Выберите файлы, которые еще не выбирали');
            return;
        }

        setUploadedFiles([...uploadedFiles].concat(choosenFiles));
    }

    const removeChoosenFile = (filename) => {
        console.log(uploadedFiles);
        for (let i = 0; i < uploadedFiles.length; ++i) {
            if (uploadedFiles[i].name === filename) {
                uploadedFiles.splice(i, 1);
                console.log(uploadedFiles);
                setUploadedFiles([...uploadedFiles]);
            }
        }
    }

    return (
        <div>
            <input id={styles.input} type="file" accept="image/*" multiple onChange={uploadFiles} />
            <label htmlFor={styles.input}>
                <a className={styles.upload}>Загрузить файлы</a>
            </label>
            <div className={styles.images}>
                {
                    uploadedFiles?.length > 0 &&
                    uploadedFiles.map(file =>
                        <div key={file.name} className={styles.image}>
                            <div className={styles.imageName}>{file.name}</div>
                            <Button classNames={styles.removeButton} callback={() => removeChoosenFile(file.name)}>Удалить</Button>
                        </div>)
                }
            </div>
        </div>
    )
}

export default FilesInput;