import { Component } from '@angular/core';
import { MeasurementService } from '..';

const DEFAULT_PLACEHOLDER = 'Choose Measurement files to import';

@Component({
  selector: 'measurement-upload',
  templateUrl: './measurement-upload.component.html',
  styleUrls: ['./measurement-upload.component.css'],
})
export class MeasurementUploadComponent {
  selectedFiles: FileList | null;
  label: string;
  uploading: boolean;
  showSuccess: boolean;
  showError: boolean;

  constructor(private measurementService: MeasurementService) {
    this.selectedFiles = null;
    this.label = DEFAULT_PLACEHOLDER;
    this.uploading = false;
    this.showSuccess = false;
    this.showError = false;
  }

  onFileChange(event: Event) {
    this.selectedFiles = (event.target as HTMLInputElement).files;
    if (this.selectedFiles && this.selectedFiles.length > 0) {
      this.label = `${this.selectedFiles?.length} file(s) to import`;
    } else {
      this.label = DEFAULT_PLACEHOLDER;
    }
  }

  onSubmit() {
    if (this.selectedFiles && this.selectedFiles.length > 0) {
      this.uploading = true;
      this.measurementService
        .uploadMeasurements(this.selectedFiles)
        .subscribe((response) => {
          if (response.ok) {
            this.showSuccess = true;
            this.label = DEFAULT_PLACEHOLDER;
            setTimeout(() => {
              this.showSuccess = false;
            }, 5000);
          } else {
            this.showError = true;
            setTimeout(() => {
              this.showError = false;
            }, 5000);
          }

          this.uploading = false;
        });
    }
  }
}
