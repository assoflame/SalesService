import { useMemo } from "react"


export const useSorting = (items, selectedSort) => {
    let sortedItems = useMemo(() => {
        if(selectedSort) {
            return [...items].sort((a, b) => {
                return (typeof a[selectedSort] === 'number')
                    ? a[selectedSort] - b[selectedSort]
                    : a[selectedSort].localeCompare(b[selectedSort]);
            });
        }
        return items;
    }, [items, selectedSort]);

    return sortedItems;
}