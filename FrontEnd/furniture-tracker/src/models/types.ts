export interface Subcomponent {
  id?: string|undefined;
  name: string;
  material: string;
  notes: string;
  count: number;
  totalQuantity: number;
  cuttingThickness: number;
  detailLength: number;
  detailWidth: number;
detailThickness: number;
  cuttingLength: number;
  cuttingWidth: number;
   finalThickness: number;
  finalLength: number;
  finalWidth: number;
  veneerInner:  string;
  veneerOuter:  string;
}

export interface Component {
  id: string;
  name: string;
  quantity: number;
  subcomponents: Subcomponent[];
}

export interface Product {
  id: string;
  name: string;
  price: number;
  components: Component[];
}

interface Size {
  length: number;
  width: number;
  thickness: number;
}
