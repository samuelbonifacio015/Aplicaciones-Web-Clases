<script setup>
import {ref} from "vue";
import FooterContent from "@/shared/presentation/components/footer-content.vue";

const drawerVisible = ref(false);
const toggleDrawer = () => {
  drawerVisible.value = !drawerVisible.value;
}
</script>

<template>
<div>
  <div>
    <pv-menubar>
      <template #start>
        <pv-button icon = "pi pi-bars" label="CatchUp" text @click="toggleDrawer"></pv-button>
      </template>
      <template #end>
        <language-switcher></language-switcher>
      </template>
    </pv-menubar>
  </div>
</div>
  <footer-content></footer-content>
</template>

<style scoped>

</style>