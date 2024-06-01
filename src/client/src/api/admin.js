import { api } from "./shared"

export const blockUser = async (userId) => {
    const response = await fetch(`${api}/admin/users/${userId}`, {
        method: "PATCH",
        credentials: "include"
    });

    if(response.ok) {
        return {success: true, payload: null}
    }

    return {success: false, payload: null};
}
