export abstract class Store<T extends Record<string, any>> {
    protected state: T;

    protected constructor() {
        const data = this.data();
        this.setup(data);
        this.state = reactive(data) as T;
    }

    protected abstract data(): T

    // eslint-disable-next-line @typescript-eslint/no-empty-function,@typescript-eslint/no-unused-vars
    protected setup(_: T): void {}

    public getState(): T {
        return readonly(this.state) as T
    }
}
