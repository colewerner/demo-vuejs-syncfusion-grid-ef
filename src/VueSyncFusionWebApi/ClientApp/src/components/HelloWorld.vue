<script setup lang="ts">
import { ref } from 'vue'

defineProps<{ msg: string }>()

const count = ref(0)
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
    <style >
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
