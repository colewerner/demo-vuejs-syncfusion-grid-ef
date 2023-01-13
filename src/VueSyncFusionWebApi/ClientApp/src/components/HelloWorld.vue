<script lang="ts">
    import { ref } from 'vue';
    import { defineComponent } from 'vue';
    import { ColumnMenu, ExcelExport, Filter, GridComponent, ColumnDirective, ColumnsDirective, Group, Page, PdfExport, Reorder, Resize, Sort, Edit, ForeignKey } from '@syncfusion/ej2-vue-grids';
    import { DataManager, UrlAdaptor, Query } from "@syncfusion/ej2-data";
    
    export default defineComponent({
      name: "HelloWorld",
      components: {
        'ejs-grid' : GridComponent,
        'e-columns' : ColumnsDirective,
        'e-column' : ColumnDirective
      },
      provide: {grid: [Group, Page, PdfExport, Reorder, Resize, Sort, Edit, ForeignKey, ColumnMenu, ExcelExport, Filter]},
      data() {
        return {
          validationRules: {
            required: true,
          },

          filterSettings: {
            type: "Excel"
          },

          pageSettings: {
            pageSize: 25,
            pageSizes: [25, 50, 100, 200],
          },

          editSettings: {
            allowEditing: true,
            allowAdding: true,
            allowDeleting: true,
            mode: "Dialog",
            showDeleteConfirmDialog: true
          },

          productDataBinding: new DataManager({
            url: "api/Product/GetProducts",
            crudUrl: "api/Product/AddEditDeleteProduct",
            adaptor: new UrlAdaptor(),
            crossDomain: true,
          }),

          categoryDataBinding: new DataManager({
            url: "api/Product/GetCategories",
            adaptor: new UrlAdaptor(),
            offline: true,
            crossDomain: true,
          }),

          supplierDataBinding: new DataManager({
            url: "api/Product/GetSuppliers",
            adaptor: new UrlAdaptor(),
            offline: true,
            crossDomain: true,
          }),

          categoryEditParams: {
            params:   {
              allowFiltering: true,
              dataSource: this.categoryDataBinding,
              fields: {text:"CategoryName", value:"CategoryId"},
              query: new Query(),
              actionComplete: () => false
            }
          },

          supplierEditParams: {
            params:   {
              allowFiltering: true,
              dataSource: this.supplierDataBinding,
              fields: {text:"SupplierName", value:"SupplierId"},
              query: new Query(),
              actionComplete: () => false
            }
          }
        }
      },
      methods: {
        exportGridToExcel() {
          let productsGrid: any = this.$refs.productsGrid;

          productsGrid.excelExport();
        },

        addProduct() {
          var productGrid: any = this.$refs.productsGrid;

          productGrid.addRecord();
        }
      }
    });
    
</script>

<template>
  <ejs-grid :dataSource="productDataBinding"
            :allowPaging="true"
            :pageSettings="pageSettings"
            :allowSorting='true'
            :allowFiltering='true'
            :allowResizing="true"
            :allowExcelExport="true"
            :allowPdfExport="true"
            :allowReordering="true"
            :filterSettings='filterSettings'
            :editSettings="editSettings"
            :showColumnMenu='true'
            :enablePersistence='false'
            ref="productsGrid">
    <e-columns>
      <e-column type='checkbox' width='50'></e-column>
      <e-column field='ProductId' headerText='Id' :isPrimaryKey="true" :isIdentity="true" width="75"></e-column>
      <e-column field='ProductName' headerText='Name' type='string' width="115"></e-column>
      <e-column field='CategoryId' headerText='Category' width='150'
                foreignKeyValue='CategoryName' :dataSource="categoryDataBinding"></e-column>
      <e-column field='SupplierId' headerText='Supplier' width='150'
                foreignKeyValue='SupplierName' :dataSource="supplierDataBinding"></e-column>
      <e-column field='QuantityPerUnit' headerText='Qty per unit' type="string" width="115"></e-column>
      <e-column field='UnitPrice' headerText='Price' type="number" width="115"></e-column>
      <e-column field='UnitsInStock' headerText='Qty in stock' type='number' width="135"></e-column>
      <e-column field='UnitsOnOrder' headerText='Units on order' type='number' width="130"></e-column>
      <e-column field="ReorderLevel" headerText='Reorder Lvl' type='number' width="130"></e-column>
      <e-column field='Discontinued' headerText='Discontinued' type='boolean' width="145" displayAsCheckBox="true" editType="booleanedit"></e-column>
    </e-columns>
  </ejs-grid>
</template>

<style scoped>
    @import "../node_modules/@syncfusion/ej2-base/styles/material.css";
    @import "../node_modules/@syncfusion/ej2-buttons/styles/material.css";
    @import "../node_modules/@syncfusion/ej2-calendars/styles/material.css";
    @import "../node_modules/@syncfusion/ej2-dropdowns/styles/material.css";
    @import "../node_modules/@syncfusion/ej2-inputs/styles/material.css";
    @import "../node_modules/@syncfusion/ej2-navigations/styles/material.css";
    @import "../node_modules/@syncfusion/ej2-popups/styles/material.css";
    @import "../node_modules/@syncfusion/ej2-splitbuttons/styles/material.css";
    @import "../node_modules/@syncfusion/ej2-vue-grids/styles/material.css";
</style>
