declare module 'isomorphic-style-loader/withStyles' {
    export interface Styles {
      [key: string]: string
    }
  
     const withStyles = (...styles: Styles[]) => <
      T extends React.ComponentClass<P, S>
    >(
      component: T,
    ): T => T
  
    export default withStyles
}

declare interface Styles {
    _insertCss(): void;
}  
  
declare module '*.scss' {
    import { Styles } from 'isomorphic-style-loader/withStyles'
  
    const value: Styles
  
    export = value
}
  
declare module '*.css' {
    import { Styles } from 'isomorphic-style-loader/withStyles'
  
    const value: Styles
  
    export = value
}