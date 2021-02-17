import { Injectable } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import swal from 'sweetalert2/dist/sweetalert2.all.min.js'


@Injectable({ providedIn: 'root' })
export class AlertService {
  constructor(private router: Router) {
  }

  simpleNotification(message: string) {
    swal.fire('', message,'success');
  }

  successNotification(message:string) {
    swal.fire('', message, 'success')
  }

  errorNotification(message: any) {
    swal.fire('', message, 'error')
  }

  alertConfirmation() {
    swal.fire({
      position: 'top-end',
      title: 'Are you sure?',
      text: 'This process is irreversible.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, go ahead.',
      cancelButtonText: 'No, let me think'
    }).then((result) => {
      if (result.value) {
        swal.fire(
          'Removed!',
          'Item removed successfully.',
          'success'
        )
      } else if (result.dismiss === swal.DismissReason.cancel) {
        swal.fire(
          'Cancelled',
          'Item is safe.)',
          'error'
        )
      }
    })
  }


  async emailNotification() {
    const { value: email } = await swal.fire({
      position: 'bottom-end',
      title: 'Input email address',
      input: 'email',
      inputPlaceholder: 'Enter your email address'
    })

    if (email) {
      swal.fire(`Entered email: ${email}`)
    }
  }
}
