import { useState } from "react"

export const useFetching = (callback, errorCallback) => {
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState('');

    const fetching = async () => {
        try {
            setIsLoading(true);
            await callback();
        } catch (e) {
            console.log(e)
            setError(e.message);
            errorCallback();
        } finally {
            setIsLoading(false);
        }
    }

    return [fetching, isLoading, error];
}