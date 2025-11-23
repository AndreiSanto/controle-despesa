import { Component, OnInit } from '@angular/core';

export interface MetaDespesaDTO {
  id?: number;
  valor: number;
  mes: number;
  ano: number;
  ativo:boolean;
}