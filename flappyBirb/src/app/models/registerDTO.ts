export class RegisterDTO {
    constructor(
        public username: string | null,
        public email: string | null,
        public password: string | null,
        public passwordConfirm: string | null
    ) { }
}
