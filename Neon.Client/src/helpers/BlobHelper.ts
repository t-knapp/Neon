 export async function toString(blob: Blob): Promise<string> {
    return new Promise((resolve, reject) => {
        const reader: FileReader = new FileReader();
        reader.onloadend = () => {
            resolve(reader.result as string);
        };
        reader.onerror = () => {
            reject(reader.error);
        };
        // result will be string @see: https://developer.mozilla.org/en-US/docs/Web/API/FileReader/result
        reader.readAsDataURL(blob);
    });
}