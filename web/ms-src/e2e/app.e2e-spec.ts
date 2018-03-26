import { MsSrcPage } from './app.po';

describe('ms-src App', function() {
  let page: MsSrcPage;

  beforeEach(() => {
    page = new MsSrcPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
