import React from "react";
import styles from "./Select.module.css"

export const Select = ({options, defaultValue, value, onChange}) => {

    return (
        <select className={styles.select}
            value={value}
            onChange = {event => onChange(event.target.value)}>
            <option className={styles.option} value="" disabled>{defaultValue}</option>
            {
                options.map(option => <option className={styles.option} key={option.value} value={option.value}>
                    {option.name}
                </option>)
            }
        </select>
    )
}