import { getAccessToken } from "./auth";
import { api } from "./shared"

export const blockUser = async (userId) => {
    console.log(userId);
    const response = await fetch(`${api}/admin/users/${userId}`, {
        method: "PATCH",
        headers : {
            'Authorization' : `Bearer ${await getAccessToken()}`
        }
    });

    if (response.ok) {
        console.log('block user success');
    } else {
        console.log('block user error');
    }
}