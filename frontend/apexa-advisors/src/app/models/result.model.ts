export type Result<T> = {
    data?: T
    isError: true
    errors: Error[]
} | {
    data: T
    isError: false
}

export interface Error {
    code: string
    message: string
}
